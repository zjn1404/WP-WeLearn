﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Models;

namespace TutorApp.Services.Interfaces.ForAPI
{
    public interface ILearningMethodService
    {
        Task<List<LearningMethod>> GetLearningMethods();
    }
}
