<?php
class assignment4model extends CI_Model{
    function __construct()
    {
        parent::__construct();
        $this->load->database();

    }

    public function getUserTable()
    {
        $query = $this->db->get('users');

        return $query->result_array();
    }

    public function getAccountData($userid)
    {
        $query = $this->db->get_where('addedaccounts', array('userid' => $userid));

        return $query->result_array();
    }
}

?>