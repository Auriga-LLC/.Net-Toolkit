namespace Auriga.Toolkit.Runtime.Abstractions;

/// <summary>
/// Operation result model with payload.
/// </summary>
/// <typeparam name="TResult">Result model type.</typeparam>
/// <typeparam name="TPayload">Result payload model type.</typeparam>
/// <example>
/// Usage in code:
/// <code>
/// var result = new OperationResultPayload&lt;SomeModel, NewModel&gt;();
/// veturn result.SetError("EntityId cannot be empty");
/// </code>
/// </example>
public sealed class Operation<TResult, TPayload> : Operation<TResult>
{
	/// <summary>
	/// Gets operation result payload.
	/// </summary>
	public TPayload? Payload { get; private set; }

	/// <summary>
	/// Sets operation result payload.
	/// </summary>
	/// <param name="payload">Payload value.</param>
	/// <returns>Current <see cref="Operation{TResult,TPayload}"/>, so that additional calls can be chained.</returns>
	public Operation<TResult, TPayload> SetPayload(TPayload payload)
	{
		Payload = payload;
		return this;
	}

	/// <summary>
	/// Sets <see cref="Operation.IsSucceed"/> to <c>true</c> and sets result data.
	/// </summary>
	/// <param name="data">Resulting data.</param>
	/// <returns>Current <see cref="Operation{TResult,TPayload}"/>, so that additional calls can be chained.</returns>
	public override Operation<TResult, TPayload> SetResult(TResult data)
	{
		IsSucceed = true;
		Result = data;
		return this;
	}

	/// <summary>
	/// Sets <see cref="Operation.IsSucceed"/> to <c>false</c> and sets/adds error.
	/// </summary>
	/// <param name="errorObj">Error.</param>
	/// <returns>Current <see cref="Operation{TResult,TPayload}"/>, so that additional calls can be chained.</returns>
	public override Operation<TResult, TPayload> SetError(Exception errorObj)
	{
		_ = base.SetError(errorObj);
		return this;
	}

	/// <summary>
	/// Sets <see cref="Operation.IsSucceed"/> to <c>false</c> and sets/adds error.
	/// </summary>
	/// <param name="errorText">Error text.</param>
	/// <returns>Current <see cref="Operation{TResult,TPayload}"/>, so that additional calls can be chained.</returns>
	public override Operation<TResult, TPayload> SetError(string? errorText)
	{
		_ = base.SetError(errorText);
		return this;
	}

	/// <summary>
	/// Sets <see cref="Operation.IsSucceed"/> to <c>false</c> and sets/adds errors.
	/// </summary>
	/// <param name="errors">Errors text list.</param>
	/// <returns>Current <see cref="Operation{TResult,TPayload}"/>, so that additional calls can be chained.</returns>
	public override Operation<TResult, TPayload> SetErrors(IEnumerable<string>? errors)
	{
		_ = base.SetErrors(errors);
		return this;
	}

	/// <summary>
	/// Sets <see cref="Operation.IsSucceed"/> to <c>false</c> and sets/adds errors.
	/// </summary>
	/// <param name="errors">Errors text list.</param>
	/// <returns>Current <see cref="Operation{TResult,TPayload}"/>, so that additional calls can be chained.</returns>
	public override Operation<TResult, TPayload> SetErrors(params string?[]? errors)
	{
		_ = base.SetErrors(errors);
		return this;
	}
}
