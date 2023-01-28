import { Container, Grid } from '@mui/material';
import { Helmet } from 'react-helmet-async';
import Footer from 'src/components/Footer';
import PageHeader from 'src/components/PageHeader';
import PageTitleWrapper from 'src/components/PageTitleWrapper';
import { TextHeader } from 'src/models/text_header';
import EditRecipe from './EditRecipe';

function ApplicationsTransactions() {
  const textHeader: TextHeader = {
    title: 'Edytuj aktywny przepis.',
    description: 'Uzupełnij poniższy formularz, aby edytować przepis.',
    isButton: false
  };
  return (
    <>
      <Helmet>
        <title>Edit Recipe</title>
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
            <EditRecipe />
          </Grid>
        </Grid>
      </Container>
      <Footer />
    </>
  );
}

export default ApplicationsTransactions;
