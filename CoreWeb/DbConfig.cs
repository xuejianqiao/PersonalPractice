using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWeb
{
    public class DbConfig
    {
        /// <summary>
        /// MySql = 0, SqlServer = 1, Sqlite = 2, Oracle = 3, PostgreSQL = 4
        /// </summary>
        public int DbType { get; set; }
        /// <summary>
        /// SystemTable = 0, Attribute = 1
        /// SystemTable 从数据库系统表查询，这种需要SA这种高权限账号，对纯MODEL有洁癖的用户适用
        /// Attribute 不受数据库限制通过实体特性读取，如果找不到主键改用Attribute方式给实体加特性
        /// </summary>
        public int InitKeyType { get; set; }
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 是否自动释放数据库，设为true我们不需要close或者Using的操作(默认false)
        /// </summary>
        public bool IsAutoCloseConnection { get; set; }
    }
}
