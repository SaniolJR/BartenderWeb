
import { useState, useEffect } from 'react';
import { Container, Grid, Paper, Button, Box } from '@mui/material';
import MissingIngredientCount from './ingredientsMissing';
import VerifiedOnly from './verifiedOnly';
import SearchByText from './searchByText.tsx';
import IngredientsBox from './Ingredients/IngredientsBox';

const apiUrl = import.meta.env.VITE_API_URL;

export default function SearchDrinkPage() {
  const [missingCount, setMissingCount] = useState<number>(0);
  const [verified, setVerified] = useState<boolean>(false);
  const [textFilter, setTextFilter] = useState<string>("");
  const [selectedIngredients, setSelectedIngredients] = useState<string[]>([]);
  const [drinksCache, setDrinksCache] = useState<any[]>([]);
  const [page, setPage] = useState(1);
  const [hasMore, setHasMore] = useState(true);

  // Fetch drinks from backend, supports pagination
  const getDrinks = async (reset = false) => {
    const currentPage = reset ? 1 : page;
    const url = new URL(`${apiUrl}/drinks`);
    url.searchParams.append("Verified", String(verified));
    url.searchParams.append("TextFilter", textFilter);
    url.searchParams.append("MissingIngredients", String(missingCount));
    url.searchParams.append("PageSize", "10");
    url.searchParams.append("Page", String(currentPage));
    selectedIngredients.forEach(ing => url.searchParams.append("Ingredients", ing));

    try {
      const res = await fetch(url.toString());
      if (!res.ok) throw new Error("Server error: " + res.status);
      const data = await res.json();
      console.log("Drinks response:", data);
      if (reset) {
        setDrinksCache(data);
        setPage(2);
      } else {
        setDrinksCache(prev => [...prev, ...data]);
        setPage(prev => prev + 1);
      }
      setHasMore(data.length === 10); // If less than page size, no more data
    } catch (err) {
      alert("Error fetching drinks: " + (err as Error).message);
    }
  };

  // Reset drinks list when filters change
  const handleApplyFilters = () => {
    setPage(1);
    setHasMore(true);
    getDrinks(true);
  };

  useEffect(() => {
    getDrinks(true);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <Container maxWidth="xl" sx={{ py: 4 }}>
      {/* Main container grid */}
      <Grid container spacing={3}>
        {/* Filters bar */}
        <Grid size={{ xs: 12 }}>
          <Paper sx={{ p: 2, display: 'flex', gap: 2, justifyContent: 'space-around' }}>
            <MissingIngredientCount number={missingCount} setNumber={setMissingCount} />
            <VerifiedOnly verified={verified} setVerified={setVerified} />
            <SearchByText
              textFilter={textFilter}
              setTextFilter={setTextFilter}
              onSearch={handleApplyFilters}
            />
            <Button onClick={handleApplyFilters}>Apply filters</Button>
          </Paper>
        </Grid>
        {/* Ingredients panel */}
        <Grid size={{ xs: 12, md: 3 }}>
          <Paper sx={{ p: 2, height: '70vh' }}>
            <IngredientsBox
              onSelectedChange={setSelectedIngredients}
              width="100%"
              height="100%"
            />
          </Paper>
        </Grid>
        {/* Drinks list panel with scroll and pagination */}
        <Grid size={{ xs: 12, md: 9 }}>
          <Paper sx={{ p: 2, height: '70vh', bgcolor: '#3e010148', overflowY: 'auto' }}>
            {drinksCache.length > 0 ? (
              <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
                {/* Render drinks */}
                {drinksCache.map((drink, idx) => (
                  <Button
                    key={drink.id || idx}
                    href={`/drink/${drink.id}`}
                    variant="outlined"
                    sx={{ justifyContent: 'flex-start', textAlign: 'left' }}
                  >
                    <Box sx={{ display: 'flex', width: '100%', height: '5vh', alignItems: 'center', justifyContent: 'space-between' }}>
                      {/* Ingredients on the left */}
                      <span style={{ fontSize: '0.9em', color: '#888', flex: 1 }}>
                        Ingredients: {Array.isArray(drink.ingredients) ? drink.ingredients.map((ing: any) => ing.name).join(', ') : ''}
                      </span>
                      {/* Name, verified, rating on the right */}
                      <span style={{ fontWeight: 'bold', marginLeft: 16 }}>
                        {drink.name} {drink.verified ? "✅" : "❌"} | {drink.averageRating ?? "No ratings yet"}
                      </span>
                    </Box>
                  </Button>
                ))}
                {/* Load more button for pagination */}
                {hasMore && (
                  <Button onClick={() => getDrinks(false)} variant="contained" sx={{ mt: 2 }}>
                    Load more
                  </Button>
                )}
              </Box>
            ) : (
              <Box sx={{ color: '#888', textAlign: 'center', mt: 2 }}>No results</Box>
            )}
          </Paper>
        </Grid>
      </Grid>
    </Container>
  );
}
