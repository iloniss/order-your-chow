import { Typography, Button, Grid } from '@mui/material';
import AddTwoToneIcon from '@mui/icons-material/AddTwoTone';


function PageHeader() {

  
  return (
    <Grid container justifyContent="space-between" alignItems="center">
      <Grid item>
        <Typography variant="h3" component="h3" gutterBottom>
          Produkty
        </Typography>
        <Typography variant="subtitle2">
          Baza dostępnych produktów spożywczych.
        </Typography>
      </Grid>
      <Grid item>
          <Button
            sx={{ mt: { xs: 2, md: 0 } }}
            href="/product/add"
            variant="contained"
            startIcon={<AddTwoToneIcon fontSize="small" />}
          >
            Dodaj produkt
          </Button>

      </Grid>
    </Grid>
  );
}

export default PageHeader;
