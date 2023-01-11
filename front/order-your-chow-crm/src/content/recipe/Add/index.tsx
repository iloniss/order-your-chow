import { Helmet } from 'react-helmet-async';
import PageHeader from '../../../components/PageHeader/index';
import PageTitleWrapper from 'src/components/PageTitleWrapper';
import { Grid, Container } from '@mui/material';
import Footer from 'src/components/Footer';
import AddRecipe from './AddRecipe';
import { TextHeader } from 'src/models/text_header';

function ApplicationsTransactions() {
  const textHeader: TextHeader = {
    title: 'Dodaj nowy przepis',
    description:
      'Uzupełnij ponizszy formularz, aby dodać nowy przepis do bazy dostępnych przepisów.',
    isButton: false
  };
  return (
    <>
      <Helmet>
        <title>Add Recipe</title>
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
            <AddRecipe />
          </Grid>
        </Grid>
      </Container>
      <Footer />
    </>
  );
}

export default ApplicationsTransactions;
