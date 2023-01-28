import { Helmet } from 'react-helmet-async';
import PageHeader from '../../../components/PageHeader/index';
import PageTitleWrapper from 'src/components/PageTitleWrapper';
import { Grid, Container } from '@mui/material';
import Footer from 'src/components/Footer';
import { TextHeader } from 'src/models/text_header';
import Recipes from './Recipes';

function ApplicationsTransactions() {
  const textHeader: TextHeader = {
    title: 'Przepisy',
    description: 'Lista dostępnych przepisów.',
    isButton: false
  };
  return (
    <>
      <Helmet>
        <title>Recipes</title>
      </Helmet>
      <PageTitleWrapper>
        <PageHeader textHeader={textHeader} />
      </PageTitleWrapper>
      <Container maxWidth="lg">
        <Grid
          container
          direction="row"
          justifyContent="center"
          alignItems="stretch"
          spacing={3}
        >
          <Grid item xs={12}>
            <Recipes />
          </Grid>
        </Grid>
      </Container>
      <Footer />
    </>
  );
}

export default ApplicationsTransactions;
