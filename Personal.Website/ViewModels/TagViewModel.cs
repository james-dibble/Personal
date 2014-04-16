// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TagViewModel.cs" company="James Dibble">
//    Copyright 2012 James Dibble
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Personal.Website.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class TagViewModel
    {
        public IEnumerable<KeyValuePair<string, int>> Tags { get; set; }

        public Type PostType { get; set; }
    }
}