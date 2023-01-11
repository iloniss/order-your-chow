import { Helmet } from 'react-helmet-async';
import PageHeader from '../../../components/PageHeader/index';
import PageTitleWrapper from 'src/components/PageTitleWrapper';
import { Grid, Container } from '@mui/material';
import Footer from 'src/components/Footer';
import EditMeasure from './EditMeasure';
import { TextHeader } from 'src/models/text_header';

function ApplicationsTransactions() {
  const textHeader: TextHeader = {
    title: 'Edytuj jednostkę miary',
    description: 'Uzupełnij ponizszy formularz, aby edytować miarę.',
    isButton: false
  };
  return (
    <>
      <Helmet>
        <title>Edit Measure</title>
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
            <EditMeasure />
          </Grid>
        </Grid>
      </Container>
      <Footer />
    </>
  );
}

export default ApplicationsTransactions;
