using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models.ForAPI.Response
{
    public class PageResponse <T>
    {
        public int currentPage { get; set; }
        public int elementPerPage { get; set; }
        public int totalPage { get; set; }
        public int totalElement {  get; set; }
        public List<T> data { get; set; }
    }
}
