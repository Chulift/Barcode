package th.co.nssol.common.framework;

public class ApplicationException extends Exception {

	/**
	 * 
	 */
	private static final long serialVersionUID = -5877506957906027174L;

	private Exception exception = null;

	private String msg = null;

	public static ApplicationException ThrowApplicationException(Exception e) {
		if (e instanceof ApplicationException) {
			return (ApplicationException) e;
		} else {
			return new ApplicationException(e);
		}
	}

	public static ApplicationException ThrowApplicationException(String msg,
			Exception e) {
		if (e instanceof ApplicationException) {
			return (ApplicationException) e;
		} else {
			return new ApplicationException(msg, e);
		}
	}

	public ApplicationException(String msg) {
		this.msg = msg;
	}

	public ApplicationException(Exception e) {
		this.exception = e;
	}

	public ApplicationException(String msg, Exception e) {
		this.msg = msg;
		this.exception = e;
	}

	@SuppressWarnings(value = {"all"})
	private ApplicationException() {
	}

	public Exception getException() {
		return exception;
	}

	public void setException(Exception exception) {
		this.exception = exception;
	}

	public String getMsg() {
		return msg;
	}

	public void setMsg(String msg) {
		this.msg = msg;
	}

	public String toString() {
		if (this.exception == null) {
			return super.toString();
		} else if (this.exception instanceof ApplicationException) {
			return super.toString();
		} else {
			return super.toString() + "//" + this.exception.toString();
		}
	}

}
