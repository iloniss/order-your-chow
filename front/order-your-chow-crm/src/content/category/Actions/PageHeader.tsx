import { Typography, Button, Grid } from '@mui/material';
import AddTwoToneIcon from '@mui/icons-material/AddTwoTone';

function PageHeader() {
  return (
    <Grid container justifyContent="space-between" alignItems="center">
      <Grid item>
        <Typography variant="h3" component="h3" gutterBottom>
          Kategorie
        </Typography>
        <Typography variant="subtitle2">
          Kategorie produktów spożywczych.
        </Typography>
      </Grid>
      <Grid item>
        <Button
          sx={{ mt: { xs: 2, md: 0 } }}
          href="/category/add"
          variant="contained"
          startIcon={<AddTwoToneIcon fontSize="small" />}
        >
          Dodaj kategorię
        </Button>
      </Grid>
    </Grid>
  );
}

export default PageHeader;
