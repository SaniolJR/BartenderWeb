import type { Dispatch, SetStateAction } from 'react'
import { TextField, InputAdornment, IconButton } from '@mui/material'
import SearchIcon from '@mui/icons-material/Search';
import ClearIcon from '@mui/icons-material/Clear';
//====================================================================================
//function handling filtring drinks by text, and running http get after clicking ENTER
//====================================================================================

//function args
type Props = {
  textFilter: string  //variable which is filter
  setTextFilter: Dispatch<SetStateAction<string>> //setter for usestate
  onSearch: () => void // additional function to run http after ENTER
}

export default function SearchByText({ textFilter, setTextFilter, onSearch }: Props) {
    
    return (        
        <TextField
          label="Search drink by name"
          value={textFilter}
          onChange={(e) => setTextFilter(e.target.value)}
          placeholder="np. Mojito"
          size="medium"
          // hendle od ENTER
          onKeyPress={(e) => {
            if (e.key === 'Enter') {
                onSearch();
            }
          }}
          sx={{ 
            width: '20vw'
          }}
          
          InputProps={{
            startAdornment: (   // loupe icon
              <InputAdornment position="start">
                <SearchIcon color="action" />
              </InputAdornment>
            ),
            endAdornment: textFilter && (   // After write X will appear, that clears everything
              <InputAdornment position="end">
                <IconButton onClick={() => setTextFilter('')} edge="end">
                  <ClearIcon />
                </IconButton>
              </InputAdornment>
            )
          }}
        />
    )
}