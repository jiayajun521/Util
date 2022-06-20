﻿using System;
using System.Threading.Tasks;
using Util.Data.Sql;
using Util.Tests.Configs;
using Xunit;

namespace Util.Data.Dapper.Tests.SqlExecutor {
    /// <summary>
    /// Sql Server Sql执行器测试 - 执行存储过程测试
    /// </summary>
    public partial class SqlServerSqlExecutorTest {
        /// <summary>
        /// 测试执行存储过程增删改操作
        /// </summary>
        [Fact]
        public async Task TestExecuteProcedureScalarAsync() {
            //定义变量
            var id = Guid.NewGuid();

            //插入数据,参数无论是否加@前缀均可
            await _sqlExecutor
                .AddParam( "@productId", id )
                .AddParam( "code", TestConfig.Value )
                .ExecuteProcedureAsync( "Products.Proc_InsertProduct" );

            //获取IntValue值
            var result = await _sqlExecutor
                .AddParam( "productId", id )
                .ExecuteProcedureScalarAsync( "Products.Proc_GetProductCode" );

            //验证
            Assert.Equal( TestConfig.Value, result );
        }
    }
}
