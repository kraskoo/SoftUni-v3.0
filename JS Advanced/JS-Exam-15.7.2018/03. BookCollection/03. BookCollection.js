class BookCollection {
	constructor(shelfGenre, room, shelfCapacity) {
		this.room = room;
		this.shelfGenre = shelfGenre;
		this.shelfCapacity = shelfCapacity;
		this.shelf = [];
	}
	
	get room() {
		return this._room;
	}
	
	set room(value) {
		if (!(value === 'livingRoom' || value === 'bedRoom' || value === 'closet')) {
			throw new Error(`Cannot have book shelf in ${value}`);
		}
		
		this._room = value;
	}
	
	addBook(bookName, bookAuthor, genre) {
		if (this.shelf.length === this.shelfCapacity) {
			this.shelf.shift();
		}
		
		this.shelf.push(genre !== undefined ? { bookName, bookAuthor, genre } : { bookName, bookAuthor });
		this.shelf = this.shelf.sort((a, b) => a.bookAuthor.localeCompare(b.bookAuthor));
		return this;
	}
	
	throwAwayBook(bookName) {
		let indexOfBook = -1;
		for (let i = 0; i < this.shelf.length; i++) {
			if (this.shelf[i].bookName === bookName) {
				indexOfBook = i;
				break;
			}
		}
		
		if (indexOfBook !== -1) {
			this.shelf.splice(indexOfBook, 1);
		}
		
		// this.shelf = this.shelf.filter(x => x.bookName !== bookName);
	}
	
	showBooks(genre) {
		let output = [ `Results for search "${genre}":` ];
		for (let entity of this.shelf) {
			if (entity.hasOwnProperty('genre') && entity['genre'] === genre) {
				output.push(`\uD83D\uDCD6 ${entity.bookAuthor} - "${entity.bookName}"`);
			}
		}
		
		return output.join('\n');
	}
	
	get shelfCondition() {
		return this.shelfCapacity - this.shelf.length;
	}
	
	toString() {
		if (this.shelf.length === 0) {
			return "It's an empty shelf";
		} else {
			let output = [ `"${this.shelfGenre}" shelf in ${this.room} contains:` ];
			for (let entity of this.shelf) {
				output.push(`\uD83D\uDCD6 "${entity.bookName}" - ${entity.bookAuthor}`);
			}
			
			return output.join('\n');
		}
	}
}