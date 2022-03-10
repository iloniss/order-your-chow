import { Typography, Button, Grid } from '@mui/material';
import AddTwoToneIcon from '@mui/icons-material/AddTwoTone';

function PageHeader() {
  return (
    <Grid container justifyContent="space-between" alignItems="center">
      <Grid item>
        <Typography variant="h3" component="h3" gutterBottom>
          Jednoski miar
        </Typography>
        <Typography variant="subtitle2">Jednostki miar składników.</Typography>
      </Grid>
      <Grid item>
        <Button
          sx={{ mt: { xs: 2, md: 0 } }}
          href="/measure/add"
          variant="contained"
          startIcon={<AddTwoToneIcon fontSize="small" />}
        >
          Dodaj miarę
        </Button>
      </Grid>
    </Grid>
  );
}

export default PageHeader;
