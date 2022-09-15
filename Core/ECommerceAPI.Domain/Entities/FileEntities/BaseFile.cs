﻿using ECommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities
{
    public class BaseFile : BaseEntity
    {
        public string FileName { get; set; }
        public string Path { get; set; }

        public string Storage { get; set; }

        [NotMapped] //migrate edilmesini istemiyorum
        public override DateTime? UpdatedDate {
            get => base.UpdatedDate; 
            set => base.UpdatedDate = value;
        }
    }
}
