namespace Auriga.Toolkit.Runtime.Abstractions;

/// <summary>
/// Operation result model.
/// </summary>
/// <typeparam name="T">Result model type.</typeparam>
/// <example>
/// Usage in code:
/// <code>
/// var result = new OperationResult&lt;SomeModel&gt;();
/// veturn result.SetError("EntityId cannot be empty");
/// </code>
/// </example>
public class Operation<T> : Operation
{
	/// <summary>
	/// Gets or sets the result entity.
	/// </summary>
	public T? Result { get; protected set; }

	/// <summary>
	/// Sets <see cref="Operation.IsSucceed"/> to <c>true</c> and sets result data.
	/// </summary>
	/// <param name="data">Resulting data.</param>
	/// <returns>Current <see cref="Operation{T}"/>, so that additional calls can be chained.</returns>
	public virtual Operation<T> SetResult(T data)
	{
		IsSucceed = true;
		Result = data;
		return this;
	}

	/// <summary>
	/// Sets <see cref="Operation.IsSucceed"/> to <c>false</c> and sets/adds error.
	/// </summary>
	/// <param name="errorObj">Error.</param>
	/// <returns>Current <see cref="Operation{T}"/>, so that additional calls can be chained.</returns>
	public override Operation<T> SetError(Exception errorObj)
	{
		_ = base.SetError(errorObj);
		return this;
	}

	/// <summary>
	/// Sets <see cref="Operation.IsSucceed"/> to <c>false</c> and sets/adds error.
	/// </summary>
	/// <param name="errorText">Error text.</param>
	/// <returns>Current <see cref="Operation{T}"/>, so that additional calls can be chained.</returns>
	public override Operation<T> SetError(string? errorText)
	{
		_ = base.SetError(errorText);
		return this;
	}

	/// <summary>
	/// Sets <see cref="Operation.IsSucceed"/> to <c>false</c> and sets/adds errors.
	/// </summary>
	/// <param name="errors">Errors text list.</param>
	/// <returns>Current <see cref="Operation{T}"/>, so that additional calls can be chained.</returns>
	public override Operation<T> SetErrors(IEnumerable<string>? errors)
	{
		_ = base.SetErrors(errors);
		return this;
	}

	/// <summary>
	/// Sets <see cref="Operation.IsSucceed"/> to <c>false</c> and sets/adds errors.
	/// </summary>
	/// <param name="errors">Errors text list.</param>
	/// <returns>Current <see cref="Operation{T}"/>, so that additional calls can be chained.</returns>
	public override Operation<T> SetErrors(params string?[]? errors)
	{
		_ = base.SetErrors(errors);
		return this;
	}
}
