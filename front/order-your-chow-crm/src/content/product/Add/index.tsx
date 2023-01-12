import { Helmet } from 'react-helmet-async';
import PageHeader from '../../../components/PageHeader/index';
import PageTitleWrapper from 'src/components/PageTitleWrapper';
import { Grid, Container } from '@mui/material';
import Footer from 'src/components/Footer';
import AddProduct from './AddProduct';
import { TextHeader } from 'src/models/text_header';

function ApplicationsTransactions() {
  const textHeader: TextHeader = {
    title: 'Dodaj nowy produkt',
    description:
      'Uzupełnij ponizszy formularz, aby dodać nowy produkt do bazy dostępnych produktów.',
    isButton: false
  };
  return (
    <>
      <Helmet>
        <title>Add Product</title>
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
            <AddProduct />
          </Grid>
        </Grid>
      </Container>
      <Footer />
    </>
  );
}

export default ApplicationsTransactions;
