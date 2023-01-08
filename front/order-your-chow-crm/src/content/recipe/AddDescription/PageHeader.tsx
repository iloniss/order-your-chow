import { Typography, Grid } from '@mui/material';

function PageHeader() {
  return (
    <Grid container justifyContent="space-between" alignItems="center">
      <Grid item>
        <Typography variant="h3" component="h3" gutterBottom>
          Dodaj opis do wybranego przepisu
        </Typography>
        <Typography variant="subtitle2">
          Uzupełnij ponizszy formularz, aby dodać opis przepisu.
        </Typography>
      </Grid>
    </Grid>
  );
}

export default PageHeader;
