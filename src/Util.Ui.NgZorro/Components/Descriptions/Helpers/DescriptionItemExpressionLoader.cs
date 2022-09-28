﻿using System.Text;
using Util.Ui.Configs;
using Util.Ui.Expressions;
using Util.Ui.NgZorro.Components.Icons.Builders;
using Util.Ui.NgZorro.Enums;

namespace Util.Ui.NgZorro.Components.Descriptions.Helpers {
    /// <summary>
    /// 描述列表项表达式加载器
    /// </summary>
    public class DescriptionItemExpressionLoader : ExpressionLoaderBase {
        /// <summary>
        /// 加载模型信息
        /// </summary>
        /// <param name="config">配置</param>
        /// <param name="info">模型表达式信息</param>
        protected override void Load( Config config, ModelExpressionInfo info ) {
            LoadTitle( config, info );
            LoadValue( config, info );
        }

        /// <summary>
        /// 加载标题
        /// </summary>
        protected virtual void LoadTitle( Config config, ModelExpressionInfo info ) {
            config.SetAttribute( UiConst.Title, info.DisplayName, false );
        }

        /// <summary>
        /// 加载值
        /// </summary>
        protected virtual void LoadValue( Config config, ModelExpressionInfo info ) {
            if ( info.IsBool ) {
                LoadBool( config, info );
                return;
            }
            if ( info.IsDate ) {
                LoadDate( config, info );
                return;
            }
            LoadText( config, info );
        }

        /// <summary>
        /// 加载布尔值
        /// </summary>
        protected virtual void LoadBool( Config config, ModelExpressionInfo info ) {
            var result = new StringBuilder();
            result.Append( new IconBuilder( config ).Type( AntDesignIcon.Check.Description() ).Attribute( "*ngIf", $"{info.SafePropertyName}" ) );
            result.Append( new IconBuilder( config ).Type( AntDesignIcon.Close.Description() ).Attribute( "*ngIf", $"!({info.SafePropertyName})" ) );
            config.SetAttribute( UiConst.Value, result.ToString(), false );
        }

        /// <summary>
        /// 加载日期值
        /// </summary>
        protected virtual void LoadDate( Config config, ModelExpressionInfo info ) {
            var format = config.GetValue( UiConst.DateFormat );
            if ( format.IsEmpty() )
                format = "yyyy-MM-dd";
            config.SetAttribute( UiConst.Value, $"{{{{{info.SafePropertyName} | date:\"{format}\"}}}}", false );
        }

        /// <summary>
        /// 加载文本值
        /// </summary>
        protected virtual void LoadText( Config config, ModelExpressionInfo info ) {
            config.SetAttribute( UiConst.Value, $"{{{{{info.SafePropertyName}}}}}", false );
        }
    }
}
