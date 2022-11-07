import React, { useState } from 'react'
import {Input} from "reactstrap";


function SearchBar({placeHolder}) {

  const [searchQuery, setSearchQuery] = useState("");

  function handle_submit(e) {
    e.preventDefault()

    if ( ! searchQuery ) return;

    window.location.href = `search/?q=${searchQuery}`;
  }

  return (
    <div>
        <form onSubmit={handle_submit} class="d-none d-md-flex ms-4"> 
          <i className="fa fa-search" style={{ marginTop: "11px", marginRight: "8px", cursor: "pointer" }} value={searchQuery} onClick={handle_submit}/>
          <Input id="searchBar" placeholder={placeHolder} type="search" value={searchQuery} onChange={(e) => setSearchQuery(e.target.value)} />
        </form>
    </div>
  )
} 

export default SearchBar;