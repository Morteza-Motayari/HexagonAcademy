using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.KeyWords
{
    public class UpdatekeyWordViewModel
    {
        public int Id { get; set; }
        [Display(Name = "عبارت")]
        public string Key { get; set; }
        public int? SportClassId { get; set; }
        public bool IsDeleted { get; set; }
    }
    public enum UpdatekeyWordResult
    {
        Success,
        KeyDuplicated,
        KeyWordNotFound
    }
}
