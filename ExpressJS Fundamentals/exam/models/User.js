const mongoose = require('mongoose');
const encryption = require('../util/encryption');

const userSchema = new mongoose.Schema({
  username: { type: mongoose.Schema.Types.String, required: true, unique: true },
  hashedPass: { type: mongoose.Schema.Types.String, required: true },
  firstName: { type: mongoose.Schema.Types.String, required: true },
  lastName: { type: mongoose.Schema.Types.String, required: true },
  salt: { type: mongoose.Schema.Types.String, required: true },
  teams: [{ type: mongoose.Schema.Types.ObjectId, ref: 'Team' }],
  profilePicture: { type: mongoose.Schema.Types.String, default: 'http://investsofia.com/wp-content/uploads/2016/11/blank-profile-picture-973460_1280.png' },
  roles: [{ type: mongoose.Schema.Types.String, enum: [ 'Admin', 'User' ] }]
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
      roles: ['Admin']
    });
  } catch (e) {
    console.log(e);
  }
};

module.exports = User;