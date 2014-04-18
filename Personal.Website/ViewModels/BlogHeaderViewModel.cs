// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BlogHeaderViewModel.cs" company="James Dibble">
//    Copyright 2012 James Dibble
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Personal.Website.ViewModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Personal.DomainModel;

    public class BlogHeaderViewModel
    {
        public const int pageLimit = 10;

        public int Year { get; set; }

        public int Month { get; set; }

        public int Page { get; set; }

        public bool HasNextPage
        {
            get
            {
                return 
                    this.Page > this.Blogs.Count() / pageLimit
                    ||
                    this.Page >= this.Blogs.Count() / pageLimit && this.Blogs.Count() % pageLimit == 0;
            }
        }

        public bool HasPreviousPage
        {
            get
            {
                return this.Page <= 1;
            }
        }

        public bool CanRemove { get; set; }

        public IEnumerable<Blog> Blogs { get; set; }

        public string GetDateString()
        {
            if (this.Month == 0 && this.Year == 0)
            {
                return string.Empty;
            }

            if (this.Month == 0)
            {
                return string.Concat(" - ", this.Year);
            }

            var date = new DateTime(this.Year == 0 ? 2000 : this.Year, this.Month, 1);

            if (this.Year == 0)
            {
                return date.ToString(" - MMMM");
            }

            return date.ToString(" - MMMM yyyy");
        }
    }
}