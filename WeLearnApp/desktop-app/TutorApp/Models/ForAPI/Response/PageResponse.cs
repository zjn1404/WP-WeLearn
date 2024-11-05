﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models.ForAPI.Response
{
    public class PageResponse <T>
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int TotalElement {  get; set; }
        public int ElementPerPage { get; set; }
        public List<T> Data { get; set; }
    }
}
