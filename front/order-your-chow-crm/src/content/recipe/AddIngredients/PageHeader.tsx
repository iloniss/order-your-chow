import { Typography, Grid } from '@mui/material';

function PageHeader() {
  return (
    <Grid container justifyContent="space-between" alignItems="center">
      <Grid item>
        <Typography variant="h3" component="h3" gutterBottom>
          Dodaj składniki do swoich przepisów
        </Typography>
        <Typography variant="subtitle2">
          Uzupełnij ponizszy formularz, aby dodać składniki do dodanych
          przepisów.
        </Typography>
      </Grid>
    </Grid>
  );
}

export default PageHeader;
