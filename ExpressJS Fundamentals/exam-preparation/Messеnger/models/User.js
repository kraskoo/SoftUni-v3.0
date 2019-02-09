const mongoose = require('mongoose');
const encryption = require('../util/encryption');

const userSchema = new mongoose.Schema({
  username: { type: String, required: true, unique: true },
  firstName: { type: String },
  lastName: { type: String },
  hashedPass: { type: String, required: true },
  salt: { type: String, required: true },
  blockedUsers: [{ type: String }],
  roles: [{ type: String, enum: [ 'Admin', 'User' ] }]
});

userSchema.method({
  authenticate: function (password) {
    return encryption.generateHashedPassword(this.salt, password) === this.hashedPass;
  }
});

const User = mongoose.model('User', userSchema);

User.seedAdminUser = async () => {
  try {
    let users = await User.find();
    if (users.length > 0) {
      return;
    }
    
    const salt = encryption.generateSalt();
    const hashedPass = encryption.generateHashedPassword(salt, 'admin');
    return User.create({
      username: 'admin',
      firstName: 'Admin',
      lastName: 'Adminov',
      salt,
      hashedPass,
      roles: [ 'Admin' ]
    });
  } catch (e) {
    console.log(e);
  }
};

module.exports = User;
