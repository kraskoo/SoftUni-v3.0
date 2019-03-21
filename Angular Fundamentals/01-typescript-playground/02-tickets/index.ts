import Ticket from "./Ticket";

function compareByDestination(first: Ticket, second: Ticket) {
  return first.destination.localeCompare(second.destination);
}

function compareByPrice(first: Ticket, second: Ticket) {
  return first.price - second.price;
}

function compareByStatus(first: Ticket, second: Ticket) {
  return first.status.localeCompare(second.status);
}

function solve(tickets: string[], sortParameter: string): Ticket[] {
  let sortFn = compareByDestination;
  if (sortParameter === 'price') {
    sortFn = compareByPrice;
  } else if (sortParameter === 'status') {
    sortFn = compareByStatus;
  }

  const array = tickets.map(ticket => {
    const parameters = ticket.split('|');
    const destination = parameters[0];
    const price = parameters[1];
    const status = parameters[2];
    return new Ticket(destination, Number(price), status);
  }).sort(sortFn);
  return array;
}

let result = solve(['Philadelphia|94.20|available',
'New York City|95.99|available',
'New York City|95.99|sold',
'Boston|126.20|departed'],
'destination');
console.log(result);
result = solve(['Philadelphia|94.20|available',
'New York City|95.99|available',
'New York City|95.99|sold',
'Boston|126.20|departed'],
'status');
console.log('\n');
console.log(result);