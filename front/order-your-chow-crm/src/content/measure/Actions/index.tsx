import { Helmet } from 'react-helmet-async';
import PageHeader from '../../../components/PageHeader/index';
import PageTitleWrapper from 'src/components/PageTitleWrapper';
import { Grid, Container } from '@mui/material';
import Footer from 'src/components/Footer';
import Measures from './Measures';
import { TextHeader } from 'src/models/text_header';

function ApplicationsTransactions() {
  const textHeader: TextHeader = {
    title: 'Jednoski miar',
    description: 'Jednostki miar składników.',
    isButton: true,
    buttonLink: '/measure/add',
    buttonDescription: 'Dodaj miarę'
  };
  return (
    <>
      <Helmet>
        <title>Measure</title>
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
            <Measures />
          </Grid>
        </Grid>
      </Container>
      <Footer />
    </>
  );
}

export default ApplicationsTransactions;
