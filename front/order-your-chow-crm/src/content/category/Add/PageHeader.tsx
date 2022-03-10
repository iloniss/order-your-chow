import { Typography, Grid } from '@mui/material';

function PageHeader() {
  return (
    <Grid container justifyContent="space-between" alignItems="center">
      <Grid item>
        <Typography variant="h3" component="h3" gutterBottom>
          Dodaj nową kategorię
        </Typography>
        <Typography variant="subtitle2">
          Uzupełnij ponizszy formularz, aby dodać nową kategorię.
        </Typography>
      </Grid>
    </Grid>
  );
}

export default PageHeader;
