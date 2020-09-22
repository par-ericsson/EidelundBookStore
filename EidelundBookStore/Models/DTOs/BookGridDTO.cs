﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace EidelundBookStore.Models.DTOs
{
    public class BookGridDTO : GridDTO
    {
        [JsonIgnore]
        public const string DefaultFilter = "all";

        public string Author { get; set; } = DefaultFilter;
        public string Genre { get; set; } = DefaultFilter;
        public string Price { get; set; } = DefaultFilter;
    }
}
