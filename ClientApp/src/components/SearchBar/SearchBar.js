import React, { useState } from 'react'

import "./searchBar.css"

function SearchBar({placeHolder}) {

  const [searchQuery, setSearchQuery] = useState("");

  function handle_submit(e) {
    e.preventDefault()

    if ( ! searchQuery ) return;

    window.location.href = `search/?q=${searchQuery}`;
  }

  return (
    <div>
        <form onSubmit={handle_submit}> 
          <i className="fa fa-search" style={{ marginRight: "10px", cursor: "pointer" }} value={searchQuery} onClick={handle_submit}/>
          <input className="search-input" placeholder={placeHolder} value={searchQuery} onChange={(e) => setSearchQuery(e.target.value)} />
          <hr className="hr"/>
        </form>

        <div style={{marginTop: "30px"}}>
          <span style={{fontSize:30}}>Recently added</span>
        </div>
    </div>
  )
} 

export default SearchBar;