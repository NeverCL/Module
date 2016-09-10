using System.Reflection;

namespace Module.NPOI
{
    /// <summary>
    /// Excel标题信息 与 属性信息
    /// </summary>
    internal class HeaderInfo
    {
        /// <summary>
        /// Excel 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Title 对应的顺序
        /// 从0开始
        /// </summary>
        public int TitleOrderId { get; set; }

        /// <summary>
        /// 对应的属性
        /// </summary>
        public PropertyInfo Property { get; set; }

        /// <summary>
        /// T 属性 对应的顺序
        /// 从0开始
        /// </summary>
        public int PropOrderId { get; set; }

        /// <summary>
        /// 导出的时候 格式化
        /// </summary>
        public string Format { get; set; }
    }
}
