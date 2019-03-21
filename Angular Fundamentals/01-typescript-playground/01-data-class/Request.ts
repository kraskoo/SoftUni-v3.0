export default class Request {
  public response: String | undefined = undefined;
  public fulfilled: Boolean = false;

  constructor(
    public method: String,
    public uri: String,
    public version: String,
    public message: String
  ) {}
}