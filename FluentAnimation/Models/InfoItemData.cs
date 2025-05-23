﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAnimation.Models
{
    class InfoItemData(string name, string address, string job, int pictureId)
    {
        public string Name { get; set; } = name;

        public string Address { get; set; } = address;

        public string Job { get; set; } = job;

        public int PictureId { get; set; } = pictureId;
    }
}
