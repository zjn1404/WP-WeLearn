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
    public class CloudinaryUploader
    {
        private readonly Cloudinary _cloudinary;
        public CloudinaryUploader()
        {
            var account = new Account(Env.GetString("CLOUD_NAME"), Env.GetString("CLOUD_KEY"), Env.GetString("CLOUD_SECRET"));
            _cloudinary = new Cloudinary(account);
        }

        public Cloudinary GetCloudinary()
        {
            return _cloudinary;
        }

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
