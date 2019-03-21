import ITicket from "./interfaces/ITicket";

export default class Ticket implements ITicket {
  constructor(
    public destination: string,
    public price: number,
    public status: string) {}
}