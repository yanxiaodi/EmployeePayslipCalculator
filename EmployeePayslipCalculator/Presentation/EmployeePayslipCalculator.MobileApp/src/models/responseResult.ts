export class ResponseResultBase {
    /**
     * Gets or sets a value indicating whether this instance is success.
     * @value
     * @c  true if this instance is success; otherwise, @c  false.
     */
    isSuccess: boolean;
    /**
     * Gets or sets the message.
     * @value
     * The message.
     */
    message: string;
}

export class ResponseResult<T> extends ResponseResultBase {
    /**
     * Gets or sets the result.
     * @value
     * The result.
     */
    result: T;
}
