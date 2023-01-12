import { Helmet } from 'react-helmet-async';
import PageHeader from '../../../components/PageHeader/index';
import PageTitleWrapper from 'src/components/PageTitleWrapper';
import { Grid, Container } from '@mui/material';
import Footer from 'src/components/Footer';
import { TextHeader } from 'src/models/text_header';

import Products from './Products';

function ApplicationsTransactions() {
  const textHeader: TextHeader = {
    title: 'Produkty',
    description: 'Baza dostępnych produktów spożywczych.',
    isButton: true,
    buttonLink: '/product/add',
    buttonDescription: 'Dodaj produkt'
  };
  return (
    <>
      <Helmet>
        <title>Product</title>
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
            <Products />
          </Grid>
        </Grid>
      </Container>
      <Footer />
    </>
  );
}

export default ApplicationsTransactions;
