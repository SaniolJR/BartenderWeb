import { useState, useEffect, useCallback } from 'react';
import { 
  Box, TextField, IconButton, List, ListItem, ListItemButton, ListItemText, Button, CircularProgress, Alert 
} from '@mui/material';
import SearchIcon from '@mui/icons-material/Search';
import CloseIcon from '@mui/icons-material/Close';

// IngredientsBox component
// props:
//   onSelectedChange: (ids: string[]) => void  // callback for external useState with selected ingredients
//   width: string | number                      // width of main box
//   height: string | number                     // height of main box
type IngredientsBoxProps = {
  onSelectedChange?: (ids: string[]) => void;
  width?: string | number;
  height?: string | number;
};

export default function IngredientsBox({ onSelectedChange, width = '100%', height = 'auto' }: IngredientsBoxProps) {
  //usestate for text filter
  const [filter, setFilter] = useState('');
  //usestate for ingredients from server
  const [ingredients, setIngredients] = useState<Array<{id: string; name: string; drinks?: any}>>([]);
  //usestate for chosen ingredients
  const [selected, setSelected] = useState<string[]>([]); 
  //usestate for current page (pagination)
  const [page, setPage] = useState(0);
  //usestate: is there more pages to load?
  const [hasMore, setHasMore] = useState(true);
  //usestate: is program downloading data?
  const [loading, setLoading] = useState(false);
  //usestate for error message during download
  const [error, setError] = useState<string | null>(null);
  
  // pageSize and endpoint URL
  const PAGE_SIZE = 20;
  const apiUrl = import.meta.env.VITE_API_URL;

  //function for downloading data from server
  const fetchIngredients = useCallback(async (isReset = false, nextPage?: number) => {
    if (loading) return;
    setLoading(true);
    setError(null);

    try {
      //get parameters for HTTP
      const targetPage = isReset ? 0 : (typeof nextPage === 'number' ? nextPage : page);
      //add them to query
      const queryParams = new URLSearchParams({
        TextFilter: filter || '', 
        PageSize: PAGE_SIZE.toString(),
        Page: targetPage.toString(),
      });
      //HTTP GET
      const res = await fetch(`${apiUrl}/ingredient?${queryParams}`);

      if (!res.ok) throw new Error(`Server Error: ${res.status}`);
      
      //recognize server response - add it to array
      const data = await res.json();
      let newItems: Array<{id: string; name: string; drinks?: any}> = [];
      newItems = data;

      //if add new ingredients or reset them
      setIngredients(prev => {
        // if reset, set new list; if not, append only new ingredients (no duplicates by id)
        const combined = isReset ? newItems : [...prev, ...newItems.filter(ni => !prev.some(pi => pi.id === ni.id))];
        return combined;
      });

      // is there more pages to load?
      setHasMore(newItems.length >= PAGE_SIZE);

      // set page number
      if (isReset) setPage(1);
      else if (typeof nextPage === 'number') setPage(nextPage);
      else setPage(prev => prev + 1);

    } catch (err: any) {
      setError(err.message || 'Błąd połączenia');
    } finally {
      setLoading(false);
    }
  }, [page, loading, filter, apiUrl]);

  // --- SEARCH/FILTER HANDLING ---
  // after clicking search: clear list, set page to 0, hasMore to true and fetch from start (reset)
  const handleSearch = () => {
    setIngredients([]); 
    setPage(0);
    setHasMore(true);
    setTimeout(() => fetchIngredients(true), 0);
  };

  // handle Enter in text field
  const handleKeyDown = (e: React.KeyboardEvent) => {
    if (e.key === 'Enter') handleSearch();
  };

  // Init - fetch first page on mount
  useEffect(() => {
    fetchIngredients(true);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []); 

  // call callback with selected ingredients on every selected change
  useEffect(() => {
    if (onSelectedChange) onSelectedChange(selected);
  }, [selected, onSelectedChange]);

  // --- SELECTION LOGIC (select/deselect ingredients) ---
  // add ingredient to selected
  const handleSelect = (id: string) => {
    if (!selected.includes(id)) setSelected([...selected, id]);
  };
  // remove ingredient from selected
  const handleDeselect = (id: string) => {
    setSelected(selected.filter(i => i !== id));
  };
  // clear all selected and filter
  const handleClearAll = () => {
    setSelected([]);
    setFilter('');
  };

  // split into selected and unselected ingredients
  const selectedIngredients = ingredients.filter(i => selected.includes(i.id));
  const unselectedIngredients = ingredients.filter(i => !selected.includes(i.id));

  // render UI
  return (
    <Box sx={{
      bgcolor: 'rgba(255, 255, 255, 0.05)',
      border: '1px solid #ccc',
      borderRadius: 2,
      p: 2,
      minHeight: height,
      width: width,
      display: 'flex',
      flexDirection: 'column'
    }}>
      <Box component="h3" sx={{ fontWeight: 600, fontSize: '1.2rem', mb: 1 }}>Ingredients</Box>
      
      <Box sx={{ display: 'flex', gap: 1, mb: 2 }}>
        <TextField
          value={filter}
          onChange={e => setFilter(e.target.value)}
          onKeyDown={handleKeyDown}
          placeholder="Filter ingredients..."
          size="small"
          sx={{ flex: 1, bgcolor: 'background.paper' }}
        />
        <Button onClick={handleClearAll} variant="outlined" color="error" size="small" sx={{ ml: 1 }}>
          Clear
        </Button>
        <IconButton onClick={handleSearch} disabled={loading} color="primary">
          <SearchIcon />
        </IconButton>
      </Box>

      {error && <Alert severity="error" sx={{ mb: 2 }}>{error}</Alert>}

      {/* WYBRANE SKŁADNIKI */}
      {selectedIngredients.length > 0 && (
        <Box sx={{ mb: 2, p: 1, bgcolor: 'rgba(255, 253, 231, 0.5)', borderRadius: 1 }}>
          <List dense>
            {selectedIngredients.map(ing => (
              <ListItem key={ing.id} divider secondaryAction={
                <IconButton edge="end" size="small" onClick={() => handleDeselect(ing.id)}>
                  <CloseIcon fontSize="small" />
                </IconButton>
              }>
                <ListItemText primary={ing.name} sx={{ pl: 2 }} />
              </ListItem>
            ))}
          </List>
        </Box>
      )}

      {/* DOSTĘPNE SKŁADNIKI (TUTAJ BYŁ BŁĄD) */}
      <Box sx={{ flex: 1, overflowY: 'auto', maxHeight: '300px' }}>
        <List dense>
          {unselectedIngredients.map((ing, index) => (
            <ListItem key={ing.id} disablePadding>
              <ListItemButton onClick={() => handleSelect(ing.id)}>
                <ListItemText primary={ing.name} />
              </ListItemButton>
            </ListItem>
          ))}
          {!loading && unselectedIngredients.length === 0 && ingredients.length === 0 && (
            <Box sx={{ p: 2, textAlign: 'center', opacity: 0.6 }}>Brak składników</Box>
          )}
        </List>
        {hasMore && (
          <Box sx={{ display: 'flex', justifyContent: 'center', p: 2, minHeight: '50px' }}>
            <Button
              onClick={() => fetchIngredients(false, page + 1)}
              disabled={loading}
              variant="contained"
              color="primary"
            >
              {loading ? <CircularProgress size={20} sx={{ mr: 1 }} /> : null}
              Załaduj więcej
            </Button>
          </Box>
        )}
      </Box>
    </Box>
  );
}