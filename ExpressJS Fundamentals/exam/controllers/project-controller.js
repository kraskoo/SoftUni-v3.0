const Team = require('../models/Team');
const Project = require('../models/Project');

module.exports = {
  createProjectGet: (req, res) => {
    res.render('project/create');
  },
  createProjectPost: (req, res) => {
    let { name, description } = req.body;
    Project.create({ name, description })
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
      let teams = Team.find();
      let projects = Project.find().where('team').equals(undefined);
      Promise.all([ teams, projects ])
        .then(([ teams, projects ]) => {
          res.render('project/all', { teams, projects });
        })
        .catch(err => {
          console.log(err);
        })
    } else {
      Project.find()
        .populate('team')
        .then(projects => {
          projects.forEach(x => {
            x.hasTeam = x.team !== undefined;
          });
          res.render('project/all', { projects });
        })
        .catch(err => {
          console.log(err);
        });
    }
  },
  distributePost: (req, res) => {
    let { team, project } = req.body;
    Team.findById(team)
      .then(team => {
        Project.findById(project)
          .then(project => {
            team.projects.push(project._id);
            project.team = team._id;
            return Promise.all([ team.save(), project.save() ]);
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
  searchPost: (req, res) => {
    let { search } = req.body;
    Project.find({ name: { $regex: search, $options: 'i' } })
      .populate('team')
      .then(projects => {
        projects.forEach(x => {
          x.hasTeam = x.team !== undefined;
        });
        res.render('project/all', { projects });
      })
      .catch(err => {
        console.log(err);
      });
  }
};