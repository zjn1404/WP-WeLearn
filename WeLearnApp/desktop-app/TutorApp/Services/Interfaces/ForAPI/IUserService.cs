using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Models;
using TutorApp.Models.ForAPI.Request;
using TutorApp.Models.ForAPI.Response;

namespace TutorApp.Services.Interfaces.ForAPI
{   


    /// <summary>
    /// The interface defines methods related to User such as login, register,...
    /// </summary>
    public interface IUserService
    {

        /// <summary>
        /// Asynchronously calls the API to register a new account.
        /// </summary>
        /// <param name="request">The request object containing the data sent by the client for account registration.</param>
        /// <returns>
        /// A `Task` representing the asynchronous operation, with a `RegisterResponse` object
        /// </returns>
        Task<RegisterResponse> RegisterAccount(RegisterRequest request);


        /// <summary>
        /// Asynchronously calls the API to login account.
        /// </summary>
        /// <param name="login">The request object containing the data sent by the client for authentication.</param>
        /// <returns>
        /// A `Task` representing the asynchronous operation, with a `LoginResponse` object
        /// </returns>
        Task<LoginResponse> LoginAccount(LoginRequest login);


        /// <summary>
        /// Asynchoronously calls the API to logout account
        /// </summary>
        /// <param name="logout">The request object containing the token in storage</param>
        /// <returns>
        /// A `Task` representing the asynchronous operation, with a `LogoutResponse` object
        /// </returns>
        Task<LogoutResponse> LogoutAccount(LogoutRequest logout);

        /// <summary>
        /// Asynchronously calls the API to request server verification of the OTP code.
        /// </summary>
        /// <param name="request">The request object containing the userId and OTP code for verification.</param>
        /// <returns>
        /// A `Task` representing the asynchronous operation, with a `VerifyResponse` object .
        /// </returns>
        Task<VerifyResponse> VerifyAccount(VerifyRequest request);

        /// <summary>
        /// Asynchronously requests the server to resend the verification token for the given user ID.
        /// </summary>
        /// <param name="_id">The user ID for which the verification token should be resent.</param>
        /// <returns>
        /// A `Task` representing the asynchronous operation, with a `ResendTokenResponse` object.
        /// </returns>
        Task<ResendTokenResponse> ResendVerifyToken(string _id);


        /// <summary>
        /// Asynchronously retrieves the user's profile using the provided authentication token.
        /// </summary>
        /// <param name="token">The authentication token used to verify the user's identity and retrieve their profile information.</param>
        /// <returns>
        /// A `Task` representing the asynchronous operation, with a `UserProfileResponse` object.
        /// </returns>
        Task<UserProfileResponse> GetMyProfile(string token);


        /// <summary>
        /// Asynchronously updates the user's profile with the provided data and authentication token.
        /// </summary>
        /// <param name="token">The authentication token used to verify the user's identity for updating the profile.</param>
        /// <param name="request">The request object containing the new profile data to update.</param>
        /// <returns>
        /// A `Task` representing the asynchronous operation, with an `UpdateProfileResponse` object.
        /// </returns>
        Task<UpdateProfileResponse> UpdateMyProfile(string token, UpdateProfileRequest request);



        /// <summary>
        /// Asynchronously updates the user's email with the provided id .
        /// </summary>
        /// <returns>
        /// A `Task` representing the asynchronous operation, with an `UpdateEmailResponse` object.
        /// </returns>
        Task<UpdateEmailResponse> UpdateUserById(UpdateEmailRequest request);


        /// <summary>
        /// Asynchronously get token unverifed-email with the provided id .
        /// </summary>
        /// <param name="userId">id of user</param>
        /// <returns> A `Task` representing the asynchronous operation, with an `UpdateEmailResponse` object.</returns>
        Task<GetUnverifiedEmailTokenResponse> GetUnverifiedEmailToken(string userId);
    }
}
