import React, { Fragment } from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import Typography from "@material-ui/core/es/Typography/Typography";

const styles = theme => ({
  root: {
    flexGrow: 1,
  },
  paper: {
    padding: theme.spacing.unit,
    textAlign: 'center',
  },
});

function FormRow(props) {
  const { classes } = props;
  return (
    <Fragment>
      {props.games.map(game => (
        <Grid key={game._id} item xs={4}>
          <Paper className={classes.paper}>
            <div>{game.title}</div>
            <img src={game.imageUrl} alt={game.title} />
            <Paper className={classes.paper}>
              {game.description}
            </Paper>
          </Paper>
        </Grid>
      ))}
    </Fragment>
  );
}

FormRow.propTypes = {
  classes: PropTypes.object.isRequired,
};

function NestedGrid(props) {
  const { classes } = props;
  const pairs = props.games.reduce(function (result, value, index, array) {
    if (index % 3 === 0) result.push(array.slice(index, index + 3));
    return result;
  }, []);
  return (
    <div className={classes.root}>
      <Typography variant="h2" color="inherit" className={classes.grow}>GAMES</Typography>
      {pairs.map((pair) => (
        <Grid container spacing={8}>
          <Grid container item xs={12} spacing={24}>
            <FormRow classes={classes} games={pair} key={pair._id} />
          </Grid>
        </Grid>
      ))}
    </div>
  );
}

NestedGrid.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(NestedGrid);