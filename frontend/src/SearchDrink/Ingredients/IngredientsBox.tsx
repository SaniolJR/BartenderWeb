import { useState, useEffect, useCallback, useRef } from 'react';
import {
  Box, TextField, IconButton, List, ListItem, ListItemText, Button, CircularProgress, Alert
} from '@mui/material';
import ListItemButton from '@mui/material/ListItemButton';
import SearchIcon from '@mui/icons-material/Search';
import CloseIcon from '@mui/icons-material/Close';


interface IngredientsBoxProps {
  onSelectedChange: (selected: string[]) => void;
  width?: string | number;
  height?: string | number;
}

export default function IngredientsBox({ onSelectedChange, width = '100%', height = 'auto' }: IngredientsBoxProps) {
  //usestate for text filter
  const [filter, setFilter] = useState('');
  //usestate for ingredients from server
  const [ingredients, setIngredients] = useState<Array<{id: string; name: string; drinks?: any}>>([]);
  //usestate for chosen ingredients (ids)
  const [selected, setSelected] = useState<string[]>([]);
  //usestate for full info about selected ingredients (cache)
  const [selectedCache, setSelectedCache] = useState<Array<{id: string; name: string; drinks?: any}>>([]);
  //usestate for current page (pagination)
  const [page, setPage] = useState(0);
  //usestate: is there more pages to load?
  const [hasMore, setHasMore] = useState(true);
  //usestate: is program downloading data?
  const [loading, setLoading] = useState(false);
  //usestate for error message during download
  const [error, setError] = useState<string | null>(null);

  const loaderRef = useRef<HTMLDivElement | null>(null);
  
  // pageSize and endpoint URL
  const PAGE_SIZE = 20;
  const apiUrl = import.meta.env.VITE_API_URL;

  //function for downloading data from server
  const fetchIngredients = useCallback(async (isReset = false, nextPage?: number) => {
    if (loading) return;
    setLoading(true);
    setError(null);

    try {
      const targetPage = isReset ? 0 : (typeof nextPage === 'number' ? nextPage : page);
      const queryParams = new URLSearchParams({
        TextFilter: filter || '',
        PageSize: PAGE_SIZE.toString(),
        Page: targetPage.toString(),
      });
      const res = await fetch(`${apiUrl}/ingredient?${queryParams}`);

      if (!res.ok) throw new Error(`Server Error: ${res.status}`);
      const data = await res.json();
      console.log('Fetched ingredients:', data);
      let newItems: Array<{id: string; name: string; drinks?: any}> = [];
      newItems = data;

      setIngredients(prev => {
        if (isReset) return newItems;
        const existingIds = new Set(prev.map(i => i.id));
        const combined = [...prev, ...newItems.filter(ni => !existingIds.has(ni.id))];
        return combined;
      });

      setHasMore(newItems.length >= PAGE_SIZE);

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
  // DO NOT reset selected ingredients!
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

  // call callback with selected ingredient names on every selected change
  useEffect(() => {
    const selectedNames = selected.map(id => {
      const item = ingredients.find(i => i.id === id) || selectedCache.find(i => i.id === id);
      return item ? item.name : null;
    }).filter((name): name is string => name !== null);
    onSelectedChange(selectedNames);
  }, [selected, ingredients, selectedCache, onSelectedChange]);

  // --- INFINITE SCROLL LOGIC ---
  useEffect(() => {
    if (!hasMore || loading) return;
    const loader = loaderRef.current;
    if (!loader) return;
    const observer = new window.IntersectionObserver(
      (entries) => {
        if (entries[0].isIntersecting && hasMore && !loading) {
          fetchIngredients(false, page + 1);
        }
      },
      { threshold: 1 }
    );
    observer.observe(loader);
    return () => observer.disconnect();
  }, [hasMore, loading, fetchIngredients, page]);

  // --- SELECTION LOGIC (select/deselect ingredients) ---
  // add ingredient to selected and cache its info
  const handleSelect = (id: string) => {
    if (!selected.includes(id)) {
      setSelected(prev => [...prev, id]);
      const ing = ingredients.find(i => i.id === id);
      if (ing && !selectedCache.some(c => c.id === id)) {
        setSelectedCache(prev => [...prev, ing]);
      }
    }
  };
  // remove ingredient from selected and cache
  const handleDeselect = (id: string) => {
    setSelected(selected.filter(i => i !== id));
    setSelectedCache(selectedCache.filter(i => i.id !== id));
  };
  // clear all selected and filter and cache
  const handleClearAll = () => {
    setSelected([]);
    setSelectedCache([]);
    setFilter('');
  };

  // selectedIngredients: always show all selected, using cache if not present in ingredients
  const selectedIngredients = selected.map(id => {
    return ingredients.find(i => i.id === id) || selectedCache.find(i => i.id === id);
  }).filter(Boolean) as {id: string; name: string; drinks?: any}[];
  // unselectedIngredients: filter by search only for unselected
  const unselectedIngredients = ingredients
    .filter(i => !selected.includes(i.id))
    .filter(i => !filter || i.name.toLowerCase().includes(filter.toLowerCase()));
  // When ingredients change, update cache for selected if new info is available
  useEffect(() => {
    setSelectedCache(prevCache => {
      const newCache = [...prevCache];
      selected.forEach(id => {
        const ing = ingredients.find(i => i.id === id);
        if (ing && !newCache.some(c => c.id === id)) {
          newCache.push(ing);
        }
      });
      return newCache;
    });
  }, [ingredients, selected]);

  // render UI
  return (
    <Box sx={{
      bgcolor: 'rgba(255, 255, 255, 0.05)',
      border: '1px solid #ccc',
      borderRadius: 2,
      p: 2,
      minHeight: height,
      height: height,
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

      {/* Chosen ingredient */}
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

      <Box sx={{ flex: 1, minHeight: 0, overflowY: 'auto' }}>
        <List dense>
          {unselectedIngredients.map((ing, index) => (
            <ListItem key={ing.id} disablePadding>
              <ListItemButton onClick={() => handleSelect(ing.id)}>
                <ListItemText primary={ing.name} />
              </ListItemButton>
            </ListItem>
          ))}
          {!loading && unselectedIngredients.length === 0 && ingredients.length === 0 && (
            <Box sx={{ p: 2, textAlign: 'center', opacity: 0.6 }}>There are no ingredients more</Box>
          )}
        </List>
        {/* Infinite scroll loader element */}
        <div ref={loaderRef} style={{ height: 32, display: hasMore ? 'block' : 'none' }} />
        {loading && (
          <Box sx={{ display: 'flex', justifyContent: 'center', p: 2, minHeight: '50px' }}>
            <CircularProgress size={20} />
          </Box>
        )}
      </Box>
    </Box>
  );
}