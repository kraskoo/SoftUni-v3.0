const User = require('../models/User');
const Team = require('../models/Team');

module.exports = {
  createTeamGet: (req, res) => {
    res.render('team/create');
  },
  createTeamPost: (req, res) => {
    let { name } = req.body;
    Team.create({ name })
      .then(() => {
        res.redirect('/');
      })
      .catch(err => {
        console.log(err);
      });
  },
  allGet: (req, res) => {
    let isAdmin = res.locals.isAdmin;
    if (isAdmin) {
      let allUsers = User.find();
      let allTeams = Team.find();
      Promise.all([ allUsers, allTeams ])
        .then(([users, teams]) => {
          res.render('team/all', { users, teams });
        })
        .catch(err => {
          console.log(err);
        })
    } else {
      Team.find()
        .populate('projects')
        .populate('members')
        .then(teams => {
          teams.forEach(x => {
            x.hasProjects = x.projects.length > 0;
            x.hasMembers = x.members.length > 0;
          });
  
          res.render('team/all', { teams });
        })
        .catch(err => {
          console.log(err);
        });
    }
  },
  distributePost: (req, res) => {
    let { user, team } = req.body;
    Team.findById(team)
      .then(team => {
        User.findById(user)
          .then(user => {
            team.members.push(user._id);
            user.teams.push(team._id);
            return Promise.all([ team.save(), user.save() ]);
          })
          .then(() => {
            res.redirect('/');
          })
          .catch(err => {
            console.log(err);
          });
      })
      .catch(err => {
        console.log(err);
      });
  },
  leavePost: (req, res) => {
    let teamId = req.params.id;
    let user = req.user;
    Team.findById(teamId)
      .then(team => {
        team.members.pull(user._id);
        req.user.teams.pull(team._id);
        return Promise.all([ team.save(), user.save() ]);
      })
      .then(() => {
        res.redirect('/user/profile');
      })
      .catch(err => {
        console.log(err);
      });
  },
  searchPost: (req, res) => {
    let { search } = req.body;
    Team.find({ name: { $regex: search, $options: 'i' } })
      .populate('projects')
      .populate('members')
      .then(teams => {
        teams.forEach(x => {
          x.hasProjects = x.projects.length > 0;
          x.hasMembers = x.members.length > 0;
        });

        res.render('team/all', { teams });
      })
      .catch(err => {
        console.log(err);
      });
  }
};