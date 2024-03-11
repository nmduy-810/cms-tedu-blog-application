using AutoMapper.Internal;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TeduBlog.Api;

public abstract class SwaggerNullableParameterFilter : IParameterFilter
{
    public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
    {
        // Kiểm tra nếu schema của tham số chưa được đánh dấu là nullable (không cho phép null)
        // và loại của tham số là nullable hoặc không phải là một value type (kiểu giá trị)
        if (!parameter.Schema.Nullable && (context.ApiParameterDescription.Type.IsNullableType() ||
                                           !context.ApiParameterDescription.Type.IsValueType))
        {
            // Đánh dấu schema của tham số là nullable (cho phép null)
            parameter.Schema.Nullable = true;
        }
    }
}