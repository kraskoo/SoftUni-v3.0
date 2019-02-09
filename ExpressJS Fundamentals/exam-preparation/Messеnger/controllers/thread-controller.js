const User = require('../models/User');
const Message = require('../models/Message');
const Thread = require('../models/Thread');
const messageHandler = require('../config/message-handler');

module.exports = {
  findPost: (req, res) => {
    let { username } = req.body;
    User.findOne({ username })
      .then(user => {
        if (user === null) {
          messageHandler(res, `No such user with username ${username}!`, 'home/index');
          return;
        }

        Thread.findOne({ users: req.user._id, users: user._id })
          .then(thread => {
            if (thread === null) {
              let newThread = new Thread({ users: [ req.user._id, user._id ] });
              newThread.save().then(() => {
                res.redirect(`/thread/${user.username}`);
              }).catch(err => {
                messageHandler(res, err.message, 'home/index');
              });
              return;
            }
            
            res.redirect(`/thread/${user.username}`);
          })
          .catch(err => {
            messageHandler(res, err.message, 'home/index');
          });
      })
      .catch(err => {
        messageHandler(res, err.message, 'home/index');
      });
  },
  otherUserGet: (req, res) => {
    let otherUser = req.params.otherUser;
    if (otherUser === req.user.username) {
      messageHandler(res, 'You cannot create thread with yourself', 'home/index');
      return;
    }

    User.findOne({ username: otherUser })
      .then(user => {
        Thread.findOne({ users: req.user._id, users: user._id })
          .then(thread => {
            Message.find({ thread: thread._id })
              .populate('user')
              .then(messages => {
                let userSides = [ req.user, user ];
                messages.forEach(x => {
                  x.side = x.user._id.toHexString() === req.user._id.toHexString() ? 'left' : 'right';
                  let content = x.content.toLowerCase();
                  x.isImage = content.startsWith('http') &&
                    (content.endsWith('.jpg') ||
                    content.endsWith('.png') ||
                    content.endsWith('.tif') ||
                    content.endsWith('.gif'));
                });
                res.render('threads/chatroom', {
                  id: thread._id,
                  users: userSides,
                  messages,
                  isBlocked: req.user.blockedUsers.includes(user.username),
                  isBlockedFromOtherUser: user.blockedUsers.includes(req.user.username)
                });
              })
              .catch(err => {
                messageHandler(res, err.message, 'home/index');
              });
          })
          .catch(err => {
            messageHandler(res, err.message, 'home/index');
          });
      })
      .catch(err => {
        messageHandler(res, err.message, 'home/index');
      });
  },
  otherUserPost: (req, res) => {
    let otherUser = req.params.otherUser;
    let body = req.body;
    let threadId = body.threadId;
    let message = body.message;
    if (!message) {
      res.redirect(`/thread/${otherUser}`);
      return;
    }

    let newMessage = new Message({ content: message, user: req.user._id, thread: threadId });
    newMessage.save()
      .then(() => {
        res.redirect(`/thread/${otherUser}`);
      })
      .catch(err => {
        messageHandler(res, err.message, 'threads/chatroom');
      });
  },
  blockUserPost: (req, res) => {
    let userToBlock = req.params.username;
    req.user.blockedUsers.push(userToBlock);
    req.user.save()
      .then(() => {
        res.redirect(`/thread/${userToBlock}`);
      })
      .catch(err => {
        res.locals.error = err.message;
        res.redirect(`/thread/${userToBlock}`);
      });
  },
  unblockUserPost: (req, res) => {
    let userToBlock = req.params.username;
    req.user.blockedUsers.pull(userToBlock);
    req.user.save()
      .then(() => {
        res.redirect(`/thread/${userToBlock}`);
      })
      .catch(err => {
        res.locals.error = err.message;
        res.redirect(`/thread/${userToBlock}`);
      });
  },
  removePost: (req, res) => {
    let threadId = req.params.threadId;
    Message.find({ thread: threadId })
      .then(messages => {
        let promises = [];
        for (let message of messages) {
          promises.push(Message.findByIdAndRemove(message._id));
        }

        promises.push(Thread.findByIdAndRemove(threadId));
        return Promise.all(promises);
      })
      .then(() => {
        res.redirect('/');
      })
      .catch(err => {
        messageHandler(res, err.message, 'home/index');
      });
  }
};