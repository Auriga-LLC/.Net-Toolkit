using System.ComponentModel;

namespace Toolkit.Extensions.Authentication.Abstractions;

public enum AuthenticationScheme
{
	[Description("Basic")]
	Basic,

	[Description("Bearer")]
	JwtBearer
}
