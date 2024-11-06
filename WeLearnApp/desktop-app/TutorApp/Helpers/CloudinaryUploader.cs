using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DotNetEnv;

namespace TutorApp.Helpers
{

    /// <summary>
    /// Helper class for uploading images to Cloudinary.
    /// This class interacts with Cloudinary's API to upload images and get the secure URL of the uploaded image.
    /// </summary>
    public class CloudinaryUploader
    {
        private readonly Cloudinary _cloudinary;


        /// <summary>
        /// Initializes a new instance of the CloudinaryUploader class.
        /// Sets up Cloudinary with the provided account credentials (using environment variables).
        /// </summary>
        public CloudinaryUploader()
        {
            var account = new Account(Env.GetString("CLOUD_NAME"), Env.GetString("CLOUD_KEY"), Env.GetString("CLOUD_SECRET"));
            _cloudinary = new Cloudinary(account);
        }


        /// <summary>
        /// Gets the Cloudinary instance.
        /// </summary>
        /// <returns>The Cloudinary instance configured with the account credentials.</returns>
        public Cloudinary GetCloudinary()
        {
            return _cloudinary;
        }


        /// <summary>
        /// Uploads an image to Cloudinary asynchronously.
        /// </summary>
        /// <param name="imagePath">The path of the image to upload.</param>
        /// <returns>A string representing the secure URL of the uploaded image, or null if the upload fails.</returns>
        public async Task<string> UploadImageAsync(string imagePath)
        {
            var cloudinary = new CloudinaryUploader().GetCloudinary();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(imagePath),
                Folder = "TutorApp/images"
            };

            try
            {
                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                //return uploadResult.SecureUri.ToString(); // Returns the URL of the uploaded image

                Debug.WriteLine(uploadResult.ToString());
                Debug.WriteLine(uploadResult.SecureUrl.ToString()); 
                return uploadResult.SecureUrl.ToString(); // Returns the URL of the uploaded image


            }
            catch (Exception ex)
            {
                // Handle the error appropriately
                Console.WriteLine($"Error uploading image: {ex.Message}");
                return null;
            }
        }



    }
}
