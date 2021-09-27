using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StorBookWebApp.Core.API.Interfaces;
using StorBookWebApp.Data;
using StorBookWebApp.DTOs.API;
using StorBookWebApp.DTOs.ImageDTOs;
using StorBookWebApp.Models;
using StorBookWebApp.Shared.ErrorResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using X.PagedList;

namespace StorBookWebApp.Core.API.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public UserService(IMapper mapper, IImageService imageService,
            UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _imageService = imageService;

        }

        public async Task<ApiResponse<UserDto>> AddUser(RegisterUserDto model)
        {
            var user = _mapper.Map<AppUser>(model);
            user.UserName = model.Email;
            var result = await _userManager.CreateAsync(user, model.Password);

            ApiResponse<UserDto> response = new();

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Regular");
                var userDTO = _mapper.Map<UserDto>(user);
                response.Data = userDTO;
                response.Succeeded = true;
                response.Message = "Registration Successful";
                return response;
            }

            foreach (var err in result.Errors)
            {
                response.Message += err.Description + ", ";
            }

            throw new BadRequestException(response.Message);
        }

        public async Task<ApiResponse<string>> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var response = new ApiResponse<string>();
            if (user == null)
            {
                response.Message = $"No record found for the user with id = {id}";
                response.Succeeded = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                return response;
            }
            await _userManager.DeleteAsync(user);
            response.Succeeded = true;
            response.Message = $"User deleted successfully";
            response.StatusCode = (int)HttpStatusCode.NoContent;
            return response;
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var response = new ApiResponse<UserDto>();
            if (user == null)
            {
                response.Data = null;
                response.Message = "User not found!";
                throw new NotFoundException(response.Message);
            }
            var result = _mapper.Map<UserDto>(user);
            return result ;
        }

        public async Task<UserDto> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var response = new ApiResponse<UserDto>();
            if (user == null)
            {
                response.Data = null;
                response.Message = "User not found!";
                throw new NotFoundException(response.Message);
            }
            var result  = _mapper.Map<UserDto>(user);
            return result;
        }

        public async Task<IList<UserDto>> GetUsers(RequestParams requestParams)
        {
            var users = await _userManager.Users.ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize); 
            var result = _mapper.Map<IList<UserDto>>(users);
            for(int i = 0; i < users.Count(); i++)
            {
                var roles = await _userManager.GetRolesAsync(users[i]);
                result[i].Roles = roles;
                
            }
            
            return result;
        }

        public async Task<IList<UserDto>> SearchUsers(string name, RequestParams param)
        {
            var users = await _userManager.Users
                .Where(q => q.FirstName.Contains(name) || q.LastName.Contains(name))
                .ToPagedListAsync(param.PageNumber, param.PageSize);
            var result = _mapper.Map<IList<UserDto>>(users);
            return result;
        }

        public async Task<ApiResponse<IdentityResult>> UpdateUser(string id, UpdateUserDto userDto)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user != null)
            {
                user.Updated = DateTime.Now;
                var model = _mapper.Map(userDto, user);
                var result = await _userManager.UpdateAsync(model);

                ApiResponse<IdentityResult> response = new();

                if (result.Succeeded)
                {
                    response.Data = result;
                    response.Message = "Record updated successfully!";
                    response.Succeeded = true;
                    response.StatusCode = (int)HttpStatusCode.NoContent;
                    return response;
                }

     
                foreach(var error in result.Errors)
                {
                    response.Message += error.Description + " ";
                }

                throw new BadRequestException(response.Message);
            }

            throw new NotFoundException("No record found");
        }

        public async Task<ApiResponse<bool>> UploadPhotoUrlAsync(string userId, AddImageDto imageDto)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new NotFoundException("User not found!");

            var imageResult = await _imageService.UploadImageAsync(imageDto.Image);
            var imageProp = new ImageAddedDTO()
            {
                PublicId = imageResult.PublicId,
                URL = imageResult.Url.ToString()
            };

            user.PhotoUrl = string.IsNullOrWhiteSpace(imageProp.URL) ? "default.jpg" : imageProp.URL;

            var result = await _userManager.UpdateAsync(user);

            ApiResponse<bool> response = new();
            if (result.Succeeded)
            {
                response.Data = true;
                return ApiResponse<bool>.Success(response.Data);
            }

            foreach(var error in result.Errors)
            {
                response.Message += error.Description + " ";
            }

            throw new BadRequestException(response.Message);
        }
    }
}
