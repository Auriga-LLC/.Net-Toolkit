namespace Auriga.Toolkit.Runtime.Abstractions;

/// <summary>
/// Operation result model.
/// </summary>
public class Operation
{
	/// <summary>
	/// Gets or sets the operation status.
	/// </summary>
	public bool IsSucceed { get; protected set; } = true;

	/// <summary>
	/// Gets or sets the list of errors on failure.
	/// </summary>
	public IEnumerable<string>? Errors { get; protected set; }

	/// <summary>
	/// Sets <see cref="IsSucceed"/> to <c>false</c> and sets/adds error.
	/// </summary>
	/// <param name="errorObj">Error.</param>
	/// <returns>Current <see cref="Operation"/>, so that additional calls can be chained.</returns>
	public virtual Operation SetError(Exception errorObj)
	{
		ArgumentNullException.ThrowIfNull(errorObj);

		return
			SetError(errorObj.Message)
			.SetError(errorObj.InnerException?.Message);
	}

	/// <summary>
	/// Sets <see cref="IsSucceed"/> to <c>false</c> and sets/adds error.
	/// </summary>
	/// <param name="errorText">Error text.</param>
	/// <returns>Current <see cref="Operation"/>, so that additional calls can be chained.</returns>
	public virtual Operation SetError(string? errorText)
	{
		IsSucceed = false;

		Errors ??= Array.Empty<string>();
		if (!string.IsNullOrWhiteSpace(errorText))
		{
			Errors = Errors.Append(errorText);
		}

		return this;
	}

	/// <summary>
	/// Sets <see cref="IsSucceed"/> to <c>false</c> and sets/adds errors.
	/// </summary>
	/// <param name="errors">Errors text list.</param>
	/// <returns>Current <see cref="Operation"/>, so that additional calls can be chained.</returns>
	public virtual Operation SetErrors(IEnumerable<string>? errors)
	{
		IsSucceed = false;

		Errors ??= Array.Empty<string>();
		string[]? errorsToAdd = errors?.ToArray();
		return errorsToAdd?.Length == 0
			? SetError("Empty errors provided")
			: SetErrors(errorsToAdd);
	}

	/// <summary>
	/// Sets <see cref="IsSucceed"/> to <c>false</c> and sets/adds errors.
	/// </summary>
	/// <param name="errors">Errors text list.</param>
	/// <returns>Current <see cref="Operation"/>, so that additional calls can be chained.</returns>
	public virtual Operation SetErrors(params string?[]? errors)
	{
		if (errors == null)
		{
			return SetError("Empty errors provided");
		}

		foreach (string? t in errors)
		{
			_ = SetError(t);
		}

		return this;
	}
}
