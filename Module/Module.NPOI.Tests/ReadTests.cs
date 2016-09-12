using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Module.NPOI.Tests
{
    public class ReadTests
    {
        [Fact]
        public void Read()
        {
            var list = "1.xls".ReadTo<TrainInfoDto>();
            Console.WriteLine(list.Count);
        }
    }

    public class TrainInfoDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [DisplayName("姓名")]
        public string UserName { get; set; }

        /// <summary>
        /// 所在单位
        /// </summary>
        [DisplayName("单位")]
        public string OrgName { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        [DisplayName("身份证")]
        public string IdCard { get; set; }

        /// <summary>
        /// 培训类型名称
        /// </summary>
        [DisplayName("培训类型")]
        public string TypeName { get; set; }

        /// <summary>
        /// 证书编号
        /// </summary>
        [DisplayName("证书编号")]
        public string CertNo { get; set; }

        /// <summary>
        /// 培训时间
        /// </summary>
        [DisplayName("培训时间")]
        public DateTime TrainDate { get; set; }

        /// <summary>
        /// 到期时间
        /// </summary>
        [DisplayName("到期时间")]
        public DateTime ExpireDate { get; set; }


        /// <summary>
        /// 证书类型
        /// </summary>
        [DisplayName("证书类型")]
        public string CertType { get; set; }

        /// <summary>
        /// 人员类别
        /// </summary>
        [DisplayName("人员类别")]
        public string UserTypeName { get; set; }

        [NotMapped]
        public long Id { get; set; }
    }

}
