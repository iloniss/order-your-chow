import { Typography, Grid, Button } from '@mui/material';
import { FC } from 'react';
import { TextHeader } from 'src/models/text_header';
import AddTwoToneIcon from '@mui/icons-material/AddTwoTone';

interface HeaderProps {
  textHeader: TextHeader;
}

const PageHeader: FC<HeaderProps> = ({ textHeader }) => {
  return (
    <Grid container justifyContent="space-between" alignItems="center">
      <Grid item>
        <Typography variant="h3" component="h3" gutterBottom>
          {textHeader.title}
        </Typography>
        <Typography variant="subtitle2">{textHeader.description}</Typography>
      </Grid>
      {textHeader.isButton ? (
        <Grid item>
          <Button
            sx={{ mt: { xs: 2, md: 0 } }}
            href={textHeader.buttonLink}
            variant="contained"
            startIcon={<AddTwoToneIcon fontSize="small" />}
          >
            {textHeader.buttonDescription}
          </Button>
        </Grid>
      ) : (
        ''
      )}
    </Grid>
  );
};

export default PageHeader;
