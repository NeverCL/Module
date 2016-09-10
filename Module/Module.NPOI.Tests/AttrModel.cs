using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.NPOI.Tests
{
    [DisplayName("sheet名称")]
    public class AttrModel
    {
        [NotMapped]
        public int Id { get; set; }

        [DisplayName("姓名")]
        public string Name { get; set; }

        [DisplayName("状态")]
        public Statu Statu { get; set; }

        [DisplayName("创建时间")]
        [DisplayFormat(DataFormatString = "yyyy年MM月dd日")]
        public DateTime Time { get; set; }
    }

    public enum Statu
    {
        [Display(Name = "激活")]
        Active,
        [DescriptionAttribute("关闭")]
        Close
    }
}
